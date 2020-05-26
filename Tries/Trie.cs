using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tries
{
    class Trie
    {
        private class Node
        {
            //public static int ALPHABET_SIZE = 26;
            private char value;
            private Dictionary<char, Node> children = new Dictionary<char, Node>();
            private bool isEndOfWord;

            public Node(char value)
            {
                this.value = value;
            }

            public bool IsEndOfWord { get => isEndOfWord; set => isEndOfWord = value; }

            public bool hasChild(char ch)
            {
                return children.Count > 0 ? children.ContainsKey(ch) : false;
            }

            public void addChild(char ch)
            {
                children.Add(ch, new Node(ch));
            }

            public Node getChild(char ch)
            {
                Node node;
                children.TryGetValue(ch, out node);
                return node;
            }
        }

        private Node root = new Node(' ');

        public void insert(string value)
        {
            value = value.ToLower();
            var currentNode = root;
            foreach (char ch in value.ToCharArray())
            {
                int index = ch - 'a';
                if (!currentNode.hasChild(ch))
                    currentNode.addChild(ch);

                currentNode = currentNode.getChild(ch);
            }

            currentNode.IsEndOfWord = true;
        }

        public void insertWithRecursion(string value)
        {
            root = new Node(' ');
            value = value.ToLower();
            insertWithRecursion(root, value.ToCharArray(), 0);
        }

        private void insertWithRecursion(Node root, char[] array, int counter)
        {
            var ch = array[counter];
            int index = ch - 'a';
            if (!root.hasChild(ch))
                root.addChild(ch);

            if (counter == (array.Length - 1))
            {
                root.IsEndOfWord = true;
                return;
            }

            insertWithRecursion(root.getChild(ch), array, ++counter);
        }
    }
}
