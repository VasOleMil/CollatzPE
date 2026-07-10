C# utility WinForms

Main Form View, use text fields as peekers for range set. 

Double clicks on range readonly fields restores bounds 0.5 or 1.0.

Double click on hystogram resolution text filed set it to 800 - image with in pixels.

![Main Form View](FormMain.png)

Unload not implemented since sequence generator is currently has limitation by Int64. Use math packeges for rigor statistics.

```csharp 
//---------------------------------------------------------------------
internal void ColStepPE()
{
  s = 0L; e = 0L; // n initialised in ColHystPE() with random value
                  // long Nmax = long.MaxValue / 4L;
  while (n != 1L) // reduced to Nmax, Collatz sequence generation
  {
    p =(n % 2L == 1L); if(p & n > Nmax) { e = 0L; break; }
    e+= p ? 0L :  1L ; s++;
    n = p ? 3L *  n  + 1L : n / 2L; 
  }
}// generate Collatz sequence and set even probability components e,s
//--------------------------------------------------------------------- 
```
