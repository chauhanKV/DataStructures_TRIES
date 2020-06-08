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
                this.Value = value;
            }

            public bool IsEndOfWord { get => isEndOfWord; set => isEndOfWord = value; }
            public char Value { get => value; set => this.value = value; }

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

            public Node[] getChildren()
            {
                return children.Values.ToArray();
            }

            public void deleteChild(char ch)
            {
                children.Remove(ch);
            }

            public bool isEmpty()
            {
                return children.Count <= 0;
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

        // O(WordLength)
        public bool contains(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            word = word.ToLower();
            return contains(root, word.ToCharArray(), 0);
        }

        private bool contains(Node root, char[] wordArray, int counter)
        {
            if (counter >= wordArray.Length)
                return root.IsEndOfWord;

            var ch = wordArray[counter];
            if (!root.hasChild(ch))
                return false;

            return contains(root.getChild(ch), wordArray, ++counter);
        }

        public void preOrderTraversal()
        {
            preOrderTraversal(root);
        }

        private void preOrderTraversal(Node root)
        {
            Console.WriteLine(root.Value);

            foreach (var child in root.getChildren())
                preOrderTraversal(child); 
        }

        public void postOrderTraversal()
        {
            postOrderTraversal(root);
        }

        private void postOrderTraversal(Node root)
        {
            foreach (var child in root.getChildren())
                postOrderTraversal(child);

            Console.WriteLine(root.Value);
        }

        public void delete(string word)
        {
            word = word.ToLower();
            delete(root, word, 0);
        }

        private void delete(Node root, string word, int index)
        {
            // o(n) -> iterating over all the possible children of root.
            //foreach (var child in root.getChildren())
            //{
            //    if (counter < wordArray.Length && child.Value == wordArray[counter])
            //        delete(child, wordArray, ++counter);
            //} //1

            //if (counter == wordArray.Length)
            //    root.IsEndOfWord = false;

            //if (root.hasChild(wordArray[counter - 1]))
            //{
            //    Node node = root.getChild(wordArray[counter - 1]);
            //    if (node.getChildren().Length <= 0 && !node.IsEndOfWord)
            //        root.deleteChild(node);
            //    else
            //        Console.WriteLine(node.Value);
            //    return;
            //}

            //Base Condition -> Stop when we reach end of word
            if (index == word.ToCharArray().Length)
            {
                root.IsEndOfWord = false;
                return;
            }

            // O(keyLength)
            var ch = word.ToCharArray()[index];
            var child = root.getChild(ch);

            if (child == null)
                return;

            delete(child, word, index + 1);

            if (child.isEmpty() && !child.IsEndOfWord)
                root.deleteChild(child.Value);
           
        }

        public void findWords(string prefix)
        {
            List<string> words = new List<string>();
            var lastNode = findLastNodeOf(prefix);
            findWords(lastNode, prefix, words);

            foreach (string word in words)
                Console.WriteLine(word);
        }

        // O(WordLength)
        private Node findLastNodeOf(string prefix)
        {
            if (prefix == null)
                return null;
            var current = root;
            foreach (var ch in prefix.ToCharArray())
            {
                var child = current.getChild(ch);
                if (child == null)
                    return null;
                current = child;
            }
            return current;
        }

        // O(n)
        private void findWords(Node node, string prefix, List<string> words)
        {
            if (node == null)
                return;

            if (node.IsEndOfWord)
                words.Add(prefix);

            foreach (var child in node.getChildren())
                findWords(child, prefix + child.Value, words);
        }

        public int getCountOfWordsInTrie()
        {
            return countOfWordsInTrie(root);  
        }

        // O(n)
        private int countOfWordsInTrie(Node node)
        {
            int wordCount = 0;

            if (node.IsEndOfWord)
                ++wordCount;

            foreach (var child in node.getChildren())
            {
                wordCount += countOfWordsInTrie(child);
            }

            return wordCount;
        }

        // O(shortword Length)
        public string longCommonPrefix(string[] words)
        {
            if (words == null || words.Length == 0)
                return "";

            var trie = new Trie();
            foreach (var word in words)
                trie.insert(word);

            var shortestWord = getShortestWord(words);
            var prefix = new StringBuilder();

            var current = trie.root;
            while (prefix.Length < shortestWord.Length)
            {
                var children = current.getChildren();
                if (children.Length != 1)
                    break;

                current = children[0];
                prefix.Append(current.Value);
            }

            return prefix.ToString();
        }

        // O(words length)
        private string getShortestWord(string[] words)
        {
            var shortestWord = words[0];

            for(int i = 0; i < words.Length; i++)
            {
                if (shortestWord.Length > words[i].Length)
                    shortestWord = words[i];
            }
            return shortestWord;
        }



    }
}
