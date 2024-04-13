# Very simple xargs style all

Each line of stdin is transposed as an argument

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
