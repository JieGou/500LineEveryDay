// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using FConsoleMain;
//
// namespace FConsoleMain
// {
//     /*
//      *给定一个整数数组 nums 和一个目标值 target，请你在该数组中找出和为目标值的那 两个 整数，并返回他们的数组下标。
//        你可以假设每种输入只会对应一个答案。但是，你不能重复利用这个数组中同样的元素。
//        示例:
//        给定 nums = [2, 7, 11, 15], target = 9
//        因为 nums[0] + nums[1] = 2 + 7 = 9
//        所以返回 [0, 1]
//        
//        来源：力扣（LeetCode）
//        链接：https://leetcode-cn.com/problems/two-sum
//        著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
//      */
//     class F1225
//     {
//         static void Main(string[] args)
//         {
//             int[] nums = {2, 7, 11, 15};
//
//             for (int i = 0; i < nums.Length; i++)
//             {
//                 Console.WriteLine("第{0}个数是{1}", i, nums[i]);
//             }
//
//             ArrayList num2 = new ArrayList();
//
//             int targetInt = 50;
//             List<int> numsList = nums.ToList();
//
//             for (int i = 0; i < numsList.Count; i++)
//             {
//                 int left = targetInt - numsList[i];
//
//                 if (numsList.Exists(m => m == left))
//                 {
//                     num2.AddRange(
//                         new int[] {i, GetKey(numsList, left)});
//                     numsList.Remove(left);
//                 }
//             }
//         }
//
//
//         public static int GetKey(List<int> list, int left)
//         {
//             int key = -1;
//
//             if (list.Exists(m => m == left))
//             {
//                 for (int i = 0; i < list.Count; i++)
//                 {
//                     if (list[i] == left)
//                     {
//                         key = i;
//                     }
//                 }
//             }
//             else
//             {
//             }
//
//             return key;
//         }
//     }
// }