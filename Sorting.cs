
public static class ArrayExtension
    {

    public static void InsertionSort(this int[] tab)
    {
        for (int i = 1; i < tab.Length; i++)
        {
            int key = tab[i];
            int j = i - 1;

            while (j >= 0 && tab[j] > key)
            {
                tab[j + 1] = tab[j];
                j--;
            }
            tab[j + 1] = key;
        }
    }

    public static void SelectionSort(this int[] tab)
    {
        for (int i = 0; i < tab.Length - 1; i++)
        {

            int min = i;
            for (int j = i + 1; j < tab.Length; j++)
            {
                if (tab[j] < tab[min])
                    min = j;
            }

            if (min != i)
            {
                int temp = tab[i];
                tab[i] = tab[min];
                tab[min] = temp;
            }
        }
    }

}