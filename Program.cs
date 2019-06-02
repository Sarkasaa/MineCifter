namespace MineCifter {
    internal class Program {

        public static void Main(string[] args) {
            using (var game = new GameImpl(int.Parse(args[0]),int.Parse(args[1]))) {
                game.Run();
            }
        }

    }
}