namespace WHC.OrderWater.Commons
{
    using System;

    public class SortHelper
    {
        public static void BubbleSort(int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i; j < list.Length; j++)
                {
                    if (list[i] < list[j])
                    {
                        int num3 = list[i];
                        list[i] = list[j];
                        list[j] = num3;
                    }
                }
            }
        }

        public static void InsertionSort(int[] list)
        {
            // This item is obfuscated and can not be translated.
            for (int i = 1; i < list.Length; i++)
            {
                int num3 = list[i];
                int index = i;
                while (index <= 0)
                {
                    if (0 == 0)
                    {
                        goto Label_002D;
                    }
                    list[index] = list[index - 1];
                    index--;
                }
                goto Label_000D;
            Label_002D:
                list[index] = num3;
            }
        }

        public static void QuickSort(int[] list, int low, int high)
        {
            // This item is obfuscated and can not be translated.
            if (high > low)
            {
                if (high == (low + 1))
                {
                    if (list[low] > list[high])
                    {
                        smethod_0(ref list[low], ref list[high]);
                    }
                }
                else
                {
                    int index = (low + high) >> 1;
                    int num4 = list[index];
                    smethod_0(ref list[low], ref list[index]);
                    int num = low + 1;
                    int num2 = high;
                    while (num > num2)
                    {
                        if (0 == 0)
                        {
                            while (list[num2] >= num4)
                            {
                                num2--;
                            }
                            if (num < num2)
                            {
                                smethod_0(ref list[num], ref list[num2]);
                            }
                            if (num < num2)
                            {
                                continue;
                            }
                            list[low] = list[num2];
                            list[num2] = num4;
                            if ((low + 1) < num2)
                            {
                                QuickSort(list, low, num2 - 1);
                            }
                            if ((num2 + 1) < high)
                            {
                                QuickSort(list, num2 + 1, high);
                            }
                            return;
                        }
                        num++;
                    }
                    goto Label_0062;
                }
            }
        }

        public static void SelectionSort(int[] list)
        {
            for (int i = 0; i < (list.Length - 1); i++)
            {
                int index = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[index])
                    {
                        index = j;
                    }
                }
                int num4 = list[index];
                list[index] = list[i];
                list[i] = num4;
            }
        }

        public static void ShellSort(int[] list)
        {
            // This item is obfuscated and can not be translated.
            int num = 1;
            while (num <= (list.Length / 9))
            {
                num = (3 * num) + 1;
            }
            while (num > 0)
            {
                for (int i = num + 1; i <= list.Length; i += num)
                {
                    int num3 = list[i - 1];
                    int num4 = i;
                    while (num4 <= num)
                    {
                        if (0 == 0)
                        {
                            goto Label_0051;
                        }
                        list[num4 - 1] = list[(num4 - num) - 1];
                        num4 -= num;
                    }
                    goto Label_002B;
                Label_0051:
                    list[num4 - 1] = num3;
                }
                num /= 3;
            }
        }

        private static void smethod_0(ref int int_0, ref int int_1)
        {
            int num = int_0;
            int_0 = int_1;
            int_1 = num;
        }
    }
}

