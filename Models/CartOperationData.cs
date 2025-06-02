namespace AutomationExerciseTests.Models
{
    public class CartOperationData
    {
        public List<int> ProductIndices { get; set; }
        public List<int> UpdateQuantities { get; set; }
        public int RemoveIndex { get; set; }
    }
}