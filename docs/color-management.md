# FDNR配色管理器文档

配色管理器另支持自定义`.xaml`文件作为背景画刷，要求如下。

- ✅ 必须指明`xmlns`属性
- ✅ 必须将一个[WPF Brush类型](https://learn.microsoft.com/zh-cn/uwp/api/windows.ui.xaml.media.brush)或[继承自Brush的类型](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/graphics-multimedia/wpf-brushes-overview)指定为**顶级元素**（[编写Brush的教程](https://learn.microsoft.com/zh-cn/dotnet/）desktop/wpf/graphics-multimedia/wpf-brushes-overview#paint-with-a-solid-color)。
- ✅ 必须被存储在`%APPDATA%\FishDeskNextReborn\Brushes`下
- ❌ 不允许在文件中携带转义符

线性渐变画刷绘制“FDNR经典”的粉蓝渐变色：

``` xaml
<LinearGradientBrush  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" StartPoint="0,0" EndPoint="1,0" SpreadMethod="Reflect" Opacity="1">
    <LinearGradientBrush.GradientStops><GradientStop Color="#FFFBC2EB" Offset="0" />
    <GradientStop Color="#FFA6C1EE" Offset="1" /></LinearGradientBrush.GradientStops>
</LinearGradientBrush>
```

纯色画刷：

``` xaml
<SolidColorBrush xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">#FFFF</SolidColorBrush>
```

![白色画刷截图](SolidWhiteBrushScreenShot.png)

图像画刷([ImageBrush](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.media.imagebrush))。以下实例将使用一张来自[https://t.alcy.cc/](https://t.alcy.cc/)的随机图片作为背景：

``` xaml
<ImageBrush xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            Stretch="None"
            ImageSource="https://t.mwm.moe/pc"/>
```

![随机图片背景](RandomImageBrush.png)