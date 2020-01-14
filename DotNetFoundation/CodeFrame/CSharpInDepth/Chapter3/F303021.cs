// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace FConsoleMain.CSharpInDepth.Chapter3
// {
//     /*
//      * 声明泛型
//      */
//     class F303021
//     {
//         static void Main(string[] args)
//         {
//           IList<>
//         }
//     }
//
//     
//     public class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
//     {
//         //声明构造函数
//         public Dictionary()
//         {
//
//         }
//         //使用类型参数声明方法
//         public void ADd(TKey key, TValue value)
//         {
//
//         }
//         public TValue this[TKey key]
//         {
//             get { }
//             set { }
//         }
//
//         IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
//         {
//             throw new NotImplementedException();
//         }
//
//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }