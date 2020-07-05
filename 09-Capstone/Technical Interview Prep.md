# Technical Interview Prep

## Big O
Big O notation is the language we use for talking about how long an algorithm takes to run. Basically, how quickly runtime grows relative to the input as the input gets larger. N is the size of the input. So if the runtime grows on the order of the size of the input (O(n)) or on the order of the square of the size of the input (O(n^2)).

      public void PrintFirstItem(int[] items)   
    {   
        Console.WriteLine(items[0]);
    }

This method runs in O(1) time (or constant time) relative to its input. The input array could be 1 item or 1,000 items, but this method would still just require one "step".

      public void PrintAllItems(int[] items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
This method runs in O(n) time (or "linear time") where n is the number of items in the array. If the array has 10 items, we have to print 10 times. 1,000, we have to print 1,000 times.

      public void PrintAllPossibleOrderedPairs(int[] items)
        {
            foreach (var firstItem in items)
            {
                foreach (var secondItem in items)
                {
                    Console.WriteLine($"{firstItem}, {secondItem}");
                }
            }
        }
Here we're nesting two loops. If our array has n items, our outer loop runs n times and our inner loop runs n times for each iteration of the outer loop. giving us n^2 total prints. Thus this method runs in O(n^2) time or quadratic time.</br>

### N could be the actual input or the size of the input

This method also has an O(n) runtime, even though one takes an integer as its input and the first example above takes an array.

    public void SayHiNTimes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("hi");
        }
    }
When calculating the big O complexity of something, drop the constants and the less significant terms. O(1+n/2+100) is still O(n) and</br>
O(n+ n^2) is still just O(n^2). </br>

When it comes to search capability we're usually talking about the "worst case".

    public bool Contains(int[] haystack, int needle)
    {
        // Does the haystack contain the needle?
        foreach (var n in haystack)
        {
            if (n == needle)
            {
                return true;
            }
        }
        return false;
    }
Here we might have 100 items in our haystack, but the first item might be the needle. In this case O(1) runtime would be the best case, and O(n) would be the worst case.</br>
### Space Complexity
Sometimes we want to optimize for using less memory instead of(or in addition to) using less time. Talking about memory cost (or "space complexity) is very similar to talking about time cost. We simply look at the total size (relative to the size of the input) of any new variables we're allocating. 

    public string[] ArrayOfHiNTimes(int n)
    {
        string[] hiArray = new string[n];
        for (int i = 0; i < n; i++)
        {
            hiArray[i] = "hi";
        }
        return hiArray;
    }
This method takes O(n) space (the size of hiArray scales with the size of the input)</br>
Usually when we talk about space complexity, we're talking about additional space. So we don't include space taken up by the inputs. For example, this method takes constant space even though the input has n items.

    public int GetLargestItem(int[] items)
    {
        int largest = int.MinValue;
        foreach (int item in items)
        {
            if (item > largest)
            {
                largest = item;
            }
        }
        return largest;
    }

Make a habit of thinking about the time and space complexity of algorithms as you design them. Before long, this will become second nature allowing you to see optimizations and potential performance issues right away. But sometimes premature optimization negatively impacts readability or coding time. For a startup it might be more important to write code that's easy to ship quickly and easy to understand later even if it's not as efficient as it could be. A great engineer knows how to strike the right balance between runtime,space, implementation time, maintainability and readability. <br>

## You should develop the skill to see time and space optimizations, as well as the wisdom to judge if those optimizations are worthwhile.

### Data Structures for Interviews
Random Access Memory (RAM)</br>

Variables are stored in RAM, sometimes called working memory. Storage is where we keep files. Thing of it as a collection of shelves or boxes, and each shelf holds 8 bits. A bit is a tiny electrical switch that can be turned on or off. But instead of on or off we call it 1 or 0. 8 bits is a byte. A processor does the real work, and it's connected to a memory controller. The memory controller does the actual reading and writing to and from RAM. It has a direct connection to each shelf of RAM. The direct connection is important. It means we can access address 0 or address 918,873 immediately without having to climb down the stack. We can access the bits at any random address in memory right away.</br>

Computers are tuned to get an extra speed boost when reading memory addresses that are close to each other. The processor has a cache it stores a copy of stuff it's recently read from RAM.

Binary Numbers</br>
Binary uses base 2 numbers. Base 10 is called decimal, Base 2 is called binary.</br>

Arrays</br>
RAM is basically an array, and our arrays access blocks of RAM when they're declared. Even with 64 bit integers, looking up the contents of a given array index is O(1) time. This fast lookup capability is the most important property of arrays. Each item in the array is the same size (takes up the same number of bytes, that's why you can't have different data types), The array is uninterrupted(contiguous in memory, there can't be any gaps in the array in memory. Arrays have very fast lookups but each item in the array needs to be the same size, and it can't grow or shrink.</br>

Strings</br>
A series of characters is called a string. Like in array, just storing char's instead of ints using ASCII alphabet encoding.