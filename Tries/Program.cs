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
            trie.insert("DOG");
            trie.insertWithRecursion("DOG");
        }
    }
}
