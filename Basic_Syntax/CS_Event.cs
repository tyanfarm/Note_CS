namespace CS_Event {
    class DataInput : EventArgs {
        public int data {get; set;}

        public DataInput(int x) {
            data = x;
        }
    }

    class UserInput {
        // ~ delegate void temp(object? sender, EventArgs args)
        public event EventHandler? eventCatcher;

        // publisher
        public void Input() {
            do {
                Console.Write("Enter number: ");
                string? s = Console.ReadLine();
                int i = Int32.Parse(s ?? string.Empty);

                eventCatcher?.Invoke(this, new DataInput(i));

            } while (true);
        }
    }

    class SquareNum {
        public void Subcriber(UserInput input) {
            input.eventCatcher += Calculate;
        }

        public void Calculate(object? sender, EventArgs e) {
            DataInput dataInput = (DataInput)e;
            int i = dataInput.data;

            Console.WriteLine($"Square Num of {i} is: {i * i}");
        }
    }
}