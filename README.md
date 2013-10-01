Safely
======

Simple extension methods for null safe accessing of data in c#.

Try it out it could change the way you do think about c# taking your code from
``` c# 
foo item;
if(obj != null){
    item = obj.prop;
}
```
to

``` c# 
var item = obj.Let(c => c.prop);
```
