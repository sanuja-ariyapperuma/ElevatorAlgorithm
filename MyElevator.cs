namespace Escalator
{
    public class MyElevator
    {
        private int _currentFloor;
        private PriorityQueue<int, int> _upQueue;
        private PriorityQueue<int, int> _upPendingQueue;
        private PriorityQueue<int, int> _downQueue;
        private PriorityQueue<int, int> _downPendingQueue;
        private bool _isMovingUpwards;
        private int _maxFloor;

        public MyElevator()
        {
            _currentFloor = 0;
            _upQueue = new PriorityQueue<int, int>();
            _downQueue = new PriorityQueue<int, int>();
            _upPendingQueue = new PriorityQueue<int, int>();
            _downPendingQueue = new PriorityQueue<int, int>();
            _isMovingUpwards = true;
        }

        public void SetMaxFloor(int maxFloor)
        {
            _maxFloor = maxFloor;
        }

        public void CallElevator(int fromFloor, bool isUp)
        {
            if (fromFloor < 0 || fromFloor > _maxFloor)
            {
                throw new ArgumentException("Invalid floor number");
            }

            if (_currentFloor == fromFloor)
            {
                return;
            }

            if (_isMovingUpwards)
            {
                if (isUp)
                {
                    if (_currentFloor < fromFloor)
                    {
                        _upQueue.Enqueue(fromFloor, fromFloor);
                    }
                    else
                    {
                        _downPendingQueue.Enqueue(fromFloor, (_maxFloor - fromFloor));
                    }
                }
                else
                {
                    _downPendingQueue.Enqueue(fromFloor, (_maxFloor - fromFloor));
                }
            }
            else
            {
                if (isUp)
                {
                    _upPendingQueue.Enqueue(fromFloor, fromFloor);
                }
                else
                {
                    if (_currentFloor < fromFloor)
                    {
                        _downPendingQueue.Enqueue(fromFloor, (_maxFloor - fromFloor));
                    }
                    else
                    {
                        _downQueue.Enqueue(fromFloor, (_maxFloor - fromFloor));
                    }
                }
            }
        }

        public void Move() 
        {

            if (_currentFloor == 0 && _upQueue.Count == 0) 
            {
                _upQueue = _upPendingQueue;
                _isMovingUpwards = true;
            }

            if (_isMovingUpwards)
            {
                if (_upQueue.Count == 0)
                {
                    if (_downPendingQueue.Count == 0) 
                    {
                        Console.WriteLine($"Elevator parked at {_currentFloor} floor");
                        return;
                    }

                    _isMovingUpwards = false;

                    moveQueue(ref _downPendingQueue,ref _downQueue);

                    _currentFloor = _downQueue.Dequeue();
                    Console.WriteLine($"Moving down to floor {_currentFloor}");

                    return;
                }
                _currentFloor = _upQueue.Dequeue();
                Console.WriteLine($"Moving up to floor {_currentFloor}");
                return; 
            }

            if (_downQueue.Count == 0)
            {
                if (_upPendingQueue.Count == 0)
                {
                    Console.WriteLine($"Elevator parked at {_currentFloor} floor");
                    return;
                }

                _isMovingUpwards = true;
                moveQueue(ref _upPendingQueue, ref _upQueue);
            }

            _currentFloor = _downQueue.Dequeue();
            Console.WriteLine($"Moving down to floor {_currentFloor}");
        }

        public void moveQueue(ref PriorityQueue<int, int> from,ref PriorityQueue<int, int> to) 
        {

            foreach (var item in from.UnorderedItems)
            {
                to.Enqueue(item.Element, item.Priority);
            }
            from.Clear();
        }
    }
}
