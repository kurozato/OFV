# OFV

This App Export Open folders list in JSON format.

```json
[
    {"name": "Temp", "path": "D:\\Temp"},
    {"name": "GitHub", "path": "D:\\GitHub"}
]
```

run Windows only.    
use COM object.    

## How To Use
- Excute -> Export to the app folder.

- Command line options：    
  OFV.exe "D:\Output" /r    
  - output directory option    
    Export to output directory    
  - /r option    
    Output file name to Console.    

## Command line sample C#

```cs

var process = new System.Diagnostics.Process();
process.StartInfo.FileName = "OFV.exe";
process.StartInfo.Arguments = "\"D:\\Output\" /r";

process.StartInfo.UseShellExecute = false;
process.StartInfo.CreateNoWindow = true;
process.StartInfo.RedirectStandardOutput = true;

process.Start();

var file = process.StandardOutput.ReadToEnd();

process.WaitForExit();
process.Close();
```

## Japanese
開いているフォルダをJSON形式で出力するアプリ。    
Windowsのみ動作し、COMオブジェクトを使用している。    
他のアプリから叩いて使う想定。    

