# Very simple xargs style all

Each line of stdin is transposed as an argument

- All args after  `--` transposed to arguments
- All args before `--` used as internal/zargs flags
- `-v` verbose
- single `_` -> ...arg1, arg2, args3
- inline string expansion "somecmd __"  -> "some arg1 arg2 arg3"
- inline string expansion "somecmd _,"  -> "some arg1,arg2,arg3"
- inline string expansion "somecmd _+"  -> "some arg1+arg2+arg3"
- the first arg is assumed to be executable

file.txt
```
10
20
30
```

```
cat file.text | zargs add.exe _
add.exe 10 20 30
60
```
