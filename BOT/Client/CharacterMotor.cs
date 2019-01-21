using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using Console = Colorful.Console;

namespace BOT {

    public class CharacterMotor {

        private NetworkManager _networkManager;

        private List<Vector3> _inputs = new List<Vector3>();
        private int _currentInputIndex = 0;
        private Vector3 _currentInput = Vector3.Zero;
        private Vector3 _currentPosition = Vector3.Zero;

        private Timer _movementTimer;
        private Timer _directionTimer;

        public CharacterMotor(NetworkManager networkManager) {
            Console.WriteLine("...Initializing CharacterMotor", Color.LightSkyBlue);

            this._networkManager = networkManager;

            _inputs.Add(Vector3.Right);
            _inputs.Add(Vector3.Forward);
            _inputs.Add(Vector3.Left);
            _inputs.Add(Vector3.Backward);

            Console.WriteLine("...Successfully initialized CharacterMotor", Color.LightSeaGreen);
        }

        public void StartMovement() {
            Console.WriteLine("Movement started!", Color.LightSkyBlue);
            _movementTimer = new Timer(20);
            _movementTimer.Elapsed += Move;
            _movementTimer.AutoReset = true;
            _movementTimer.Enabled = true;

            _directionTimer = new Timer(1000);
            _directionTimer.Elapsed += ChangeDirection;
            _directionTimer.AutoReset = true;
            _directionTimer.Enabled = true;

            _currentInput = _inputs[0];
        }

        public void StopMovement() {
            Console.WriteLine("Movement stopped!", Color.LightSkyBlue);
            _movementTimer.Stop();
            _movementTimer.Dispose();

            _directionTimer.Stop();
            _directionTimer.Dispose();
        }

        private void ChangeDirection(Object source, ElapsedEventArgs e) {
            _currentInput = GetNextInput();
        }

        private void Move(Object source, ElapsedEventArgs e) {
            ShiftServerData data = new ShiftServerData();

            data.PlayerInput = new SPlayerInput();
            data.PlayerInput.PosX = _currentInput.posX;
            data.PlayerInput.PosZ = _currentInput.posZ;
            data.PlayerInput.PressTime = 0.02f;

            _networkManager.mss.SendMessage(MSPlayerEvent.Move, data);
        }

        private Vector3 GetNextInput() {
            _currentInputIndex++;

            if (_currentInputIndex > _inputs.Count - 1) {
                _currentInputIndex = 0;
            }

            return _inputs[_currentInputIndex];
        }

    }

}
