namespace LoadingTree
{
    public struct LoadingResult
    {
        public bool success;
        public string error;
        
        public static LoadingResult Success() => new() {success = true, error = null};
        public static LoadingResult Error(string error) => new() {success = false, error = error};
    }
}