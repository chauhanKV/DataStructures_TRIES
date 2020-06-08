using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tries
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            trie.insert("care");

            Console.WriteLine("-----------Pre Order Traversal ---------------");
            trie.preOrderTraversal();
            Console.WriteLine("-----------Post Order Traversal ---------------");
            trie.postOrderTraversal();

            trie.insert("car");
            trie.insert("carefull");
            trie.insert("card");
            trie.insert("egg");
            //trie.insertWithRecursion("CANADA");

            Console.WriteLine("Does trie contains word 'CAN' ?" + trie.contains("CAN"));

            trie.delete("CAN");

            Console.WriteLine("Does trie contains word 'CANADA' ?" + trie.contains("CANADA"));
            Console.WriteLine("Does trie contains word 'CAN' ?" + trie.contains("CAN"));

            Console.WriteLine("----------- AutoComplete ---------------");
            trie.findWords("");
            Console.WriteLine("Word count in Trie is : " + trie.getCountOfWordsInTrie());

            var commonPrefix = trie.longCommonPrefix(new string[4] { "car", "care", "carefull", "cargo"});
            Console.WriteLine("Most common prefix is : " + commonPrefix);

            Console.ReadLine();
        }
    }
}
