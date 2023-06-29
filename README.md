# Farlight84 Simple EAC Bypass

**Detected: EAC has started sending/receiving packets and if it returns null or 0, EAC will try many times at different times and eventually you will be banned.**

[[Release] Farlight84 Simple EAC Bypass]([docs/CONTRIBUTING.md](https://www.unknowncheats.me/forum/other-fps-games/585130-farlight84-simple-eac-bypass.html))

It's simple and **based on UC member idea**, I forgot the name 
Simple way: Editing the json file before anti-cheat opened then return the default values after EAC down.

Also, it will read the real game path from registry then combine other directories.
```
var key = "SOFTWARE\\Wow6432Node\\Valve\\Steam";
using var steamKey = Registry.LocalMachine.OpenSubKey(key);
```

**Instructions:**
1. Restore your original settings.json files (by deleting it and restore it again using steam recovery).
2. Launch the exe file.
3. Open your game from steam or desktop shortcut.

![Screenshot](/screenshots/1.png)
![Screenshot](/screenshots/2.png)

After game start and you're at lobby, enter tdm or tg for the first login only to find the red flag message, and as most members said it's useless. So, press OK , then inject your favorites dll.

![Screenshot](/screenshots/3.png)
![Screenshot](/screenshots/4.png)
