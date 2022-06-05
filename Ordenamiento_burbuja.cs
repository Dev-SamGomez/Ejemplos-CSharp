int[] Sort = new int[] {1,3,5,6,8,9,2,4,7}
int t;
for (int a = 1; a < Sort.Length; a++)
    for (int b = Sort.Length - 1; b >= a; b--)
    {
        if (Sort[b - 1] > Sort[b])
        {
            t = Sort[b - 1];
            Sort[b - 1] = Sort[b];
            Sort[b] = t;
        }
    }