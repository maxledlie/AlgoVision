using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModels
{
    /// <summary>
    /// This class handles the interaction between the UI thread and sorting algorithms
    /// with many steps which can be played, paused, stopped and stepped through.
    /// </summary>
    public class PlayerViewModel : ViewModelBase
    {
        private PlayerStatus _status;
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private bool _step;
        private int _playbackSpeed = 100;

        private const int _pauseLoopTime = 100;

        public event EventHandler ProgressUpdate;

        public PlayerViewModel()
        {
            _status = PlayerStatus.Stopped;
        }

        public PlayerStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public int PlaybackSpeed
        {
            get => _playbackSpeed;
            set => SetProperty(ref _playbackSpeed, value);
        }
        
        public bool PlayEnabled  => _status != PlayerStatus.Running;
        public bool PauseEnabled => _status == PlayerStatus.Running;
        public bool StopEnabled  => _status != PlayerStatus.Stopped;
        public bool StepEnabled  => _status != PlayerStatus.Running;

        public bool Stopped => _status == PlayerStatus.Stopped;        

        public void SetStatus(PlayerStatus statusType)
        {
            _step = false;
            _status = statusType;
            OnPropertyChanged("PlayEnabled");
            OnPropertyChanged("PauseEnabled");
            OnPropertyChanged("StopEnabled");
            OnPropertyChanged("StepEnabled");
            OnPropertyChanged("Stopped");
        }

        public async Task Sort<T>(T[] arr, ObservableSortingAlgorithm alg)           
            where T : IComparable
        {
            bool wasPaused = _status == PlayerStatus.Paused;

            SetStatus(PlayerStatus.Running);

            if (wasPaused)
                return;

            alg.ProgressUpdate += (s, e) =>
            {
                Application.Current.Dispatcher.Invoke(delegate { UpdateProgress(); });
                Thread.Sleep(1000 / _playbackSpeed);
                while (_status == PlayerStatus.Paused)
                {
                    _ct.WaitHandle.WaitOne(_pauseLoopTime);
                }
            };            

            _cts = new CancellationTokenSource();
            _ct = _cts.Token;

            try
            {
                await Task.Run(() =>
                {
                    alg.Sort(arr);
                    SetStatus(PlayerStatus.Stopped);
                });
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                SetStatus(PlayerStatus.Stopped);
                _cts.Dispose();
            }
        }                        

        public void Stop()
        {
            _cts.Cancel();
            SetStatus(PlayerStatus.Stopped);
        }

        public void Pause()
        {
            SetStatus(PlayerStatus.Paused);            
        }

        public async Task Step<T>(T[] arr, ObservableSortingAlgorithm alg)
            where T : IComparable
        {
            if (_status == PlayerStatus.Stopped)
                await Sort(arr, alg);

            _step = true;
            _status = PlayerStatus.Running;
        }

        private void UpdateProgress()
        {
            ProgressUpdate?.Invoke(this, null);
            _ct.ThrowIfCancellationRequested();
            if (_step)
                Pause();
        }
    }
}
