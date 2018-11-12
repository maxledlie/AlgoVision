using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const int _arraySize = 100;

        private int[] _array;
        private int[] _savedArray;
        private ObservableCollection<ObservableSortingAlgorithm> _algorithms;
        private ObservableSortingAlgorithm _selectedAlgorithm;
        private Random _random;
        private PlayerViewModel _player;

        public MainViewModel()
        {
            _array = new int[_arraySize];
            _random = new Random();
            _player = new PlayerViewModel();
            _player.ProgressUpdate += (s, e) => PublishState();
            InitializeCommands();
            PrepareAlgorithms();
            _array = Enumerable.Range(1, _arraySize).ToArray();
            Shuffle();
        }

        private void PrepareAlgorithms()
        {
            Algorithms = new ObservableCollection<ObservableSortingAlgorithm>()
            {
                new QuickSort(),
                new MergeSort(),
                new InsertionSort(),
                new BubbleSort(),               
            };
            SelectedAlgorithm = _algorithms[0];
        }

        public int[] Array
        {
            get => _array;
            set => SetProperty(ref _array, value);
        }

        public ObservableCollection<ObservableSortingAlgorithm> Algorithms
        {
            get => _algorithms;
            set => SetProperty(ref _algorithms, value);
        }    

        public ObservableSortingAlgorithm SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            set => SetProperty(ref _selectedAlgorithm, value);
        }

        public PlayerViewModel Player
        {
            get => _player;
            set => SetProperty(ref _player, value);
        }

        public DelegateCommand ShuffleCommand { get; private set; }
        public DelegateCommand SortCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }
        public DelegateCommand StepCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }

        private void Shuffle()
        {
            _array.Shuffle();
            PublishState();
        }

        private void InitializeCommands()
        {
            ShuffleCommand = new DelegateCommand((s) => { Shuffle(); }, null);
            SortCommand = new DelegateCommand(async (s) => { await Sort(); }, null);
            PauseCommand = new DelegateCommand((s) => { Pause(); }, null);
            StepCommand = new DelegateCommand(async (s) => { await Step(); }, null);
            StopCommand = new DelegateCommand((s) => { Stop(); }, null);            
        }

        private async Task Sort()
        {
            if (_player.Status == PlayerStatus.Stopped)
                SaveState();

            await _player.Sort(_array, _selectedAlgorithm);
        }

        private void Stop()
        {
            _player.Stop();
            Revert();
        }

        private void Pause()
        {
            _player.Pause();
            PublishState();
        }

        private async Task Step()
        {
            if (_savedArray == null)
                _savedArray = _array.Copy();

            await _player.Step(_array, _selectedAlgorithm);
        }

        private void SaveState()
        {
            _savedArray = _array.Copy();
        }

        private void PublishState()
        {
            Array = _array;
        }

        private void Revert()
        {
            Array = _savedArray;
        }
    }
}
