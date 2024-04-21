# FishDeskNext:Reborn

[![.NET Core Desktop](https://github.com/liziyu0714/FishDeskNextReborn/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/liziyu0714/FishDeskNextReborn/actions/workflows/dotnet-desktop.yml)

你还在为在教室里玩电脑被老师发现而苦恼吗?你需要FishDeskNext:Reborn.

FishDeskNext:Reborn 基于FishDeskNext 重新设计,当程序启动时,你的Windows会切换到下一个桌面.

Windows的任务视图是一个隐蔽窗口的好方法,可是在基于触控的教室电脑上,通过任务栏图标或屏幕边缘左划手势启动任务视图,再点击进行切换,非常的不方便,一不小心就会被老师发现.

于是,FishDeskNext应运而生,它通过模拟 Ctrl+Windows+Left 组合键切换到下一个桌面,解决了上述方法速度过慢的问题.

基于FishDeskNext,我开发了FishDeskNext:Reborn,加入了JumpList功能,即可以在任务栏上右键(对应触控的长按),唤出具有切回上个桌面功能的菜单.

## 用法

## 自己编译
编译需要在Windows7及以上进行,需要安装.Net 8 SDK:[dotnet](https://dotnet.microsoft.com/)
先克隆此仓库
```
git clone https://github.com/liziyu0714/FishDeskNextReborn.git
cd .\FishDeskNextReborn 
```

运行:
```
dotnet run
```

发布
```
dotnet publish
```