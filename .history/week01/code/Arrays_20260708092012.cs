public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // Step 1: Create an array with 'length' elements.
        // Step 2: Loop from 0 to length-1.
        // Step 3: For each index i, calculate number * (i+1).
        // Step 4: Store the result in the array.
        // Step 5: Return the array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// For example, if the data is List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3  
    /// then the list after the function runs should be List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // Step 1: Find the split index = data.Count - amount.
        // Step 2: Slice the list into two parts using GetRange.
        // Step 3: Clear the original list.
        // Step 4: Add the right part first, then the left part.
        // Step 5: The list is now rotated in place.

        int splitIndex = data.Count - amount;
        List<int> rightPart = data.GetRange(splitIndex, amount);
        List<int> leftPart = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}
