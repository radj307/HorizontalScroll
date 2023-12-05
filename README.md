![horizontalscroll-banner](https://github.com/radj307/HorizontalScroll/assets/1927798/f7acf6f9-535a-43d7-bf83-88725dd2cb0b)

Adds better horizontal scrolling support to WPF, and some useful behaviors for improving `ScrollViewer`.

| NuGet                | Version |
|----------------------|---------|
| **HorizontalScroll** | [![NuGet Status](http://img.shields.io/nuget/v/HorizontalScroll.svg?style=flat&logo=nuget&label=NuGet)](https://www.nuget.org/packages/HorizontalScroll) |

Many newer high-end mice have scroll wheels that can be tilted left or right to scroll horizontally, but WPF doesn't provide any support for them. **HorizontalScroll** provides attached events to receive these messages on a per-control basis, just like the built-in `PreviewMouseWheel` & `MouseWheel` events.

# Usage

No `xmlns` declaration is required to use **HorizontalScroll** in XAML.

> **Table of Contents**
> 
> 1. [Using `HorizontalScrollBehavior`](#behaviors)
>    1. [Application-Wide Support](#enabling-application-wide-support)
> 3. [Events](#events)

## Behaviors

`HorizontalScrollBehavior` can be attached to any `ScrollViewer` control to add support for horizontal scrolling by tilting the scroll wheel, or by holding the `Shift` key while scrolling up/down.  
You can attach it to a specific `ScrollViewer` entirely in XAML:  
```xaml
<ScrollViewer xmlns:i="http://schemas.microsoft.com/xaml/behaviors">
    <i:Interaction.Behaviors>
        <HorizontalScrollBehavior Magnitude="0.5" />
        <!--                       ▲▲▲▲▲▲▲
            Magnitude is a DependencyProperty of type double that determines how sensitive horizontal scrolling is. The
             range is 0.0 - 1.0, where 0 is no sensitivity (disabled) and 1 is maximum sensitivity. The default is 0.33.
            -->
    </i:Interaction.Behaviors>
</ScrollViewer>
```
Scrollable WPF controls like `ListBox`, `ListView`, `TreeView`, `DataGrid`, etc. are implemented using a `ScrollViewer`, so you can use `HorizontalScrollBehavior` to add support for them as well. While you *could* use a `ControlTemplate`, it's much more maintainable - not to mention faster and easier - to attach it in the code-behind with a style and an `EventSetter` instead:  
```xaml
<ListBox>
    <ListBox.Resources>
        <Style TargetType="{x:Type ScrollViewer}">
            <EventSetter Event="Loaded" Handler="ScrollViewer_Loaded" />
        </Style>
    </ListBox.Resources>
</ListBox>
```
```csharp
private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
{
    // Tip: You can set the Magnitude property (to a value or binding) in the HorizontalScrollBehavior constructor.
    Interaction
        .GetBehaviors((ScrollViewer)sender)
        .Add(new HorizontalScrollBehavior( /* magnitude: 0.5 | magnitudeBinding: new Binding() */ ));
}
```

### Enabling Application-Wide Support

The previous example showed how to attach `HorizontalScrollBehavior` to a `ListBox`'s internal `ScrollViewer`, but that approach can also be used for an entire window or application. The exact same code as the previous example will work just fine, but if you want to make your own `ScrollViewer` style later on you may want one with a key so you can inherit from it:  
```xaml
<ListBox>
    <ListBox.Resources>

        <!--  You can inherit from this style later on to avoid multiple implementations:  -->
        <Style x:Key="AttachScrollBehaviorStyle" TargetType="{x:Type ScrollViewer}">
            <EventSetter Event="Loaded" Handler="ScrollViewer_Loaded" />
        </Style>

        <!--  This style inherits from the previous one, but it's the default for all ScrollViewers:  -->
        <Style TargetType="{x:Type ScrollViewer}" BasedOn="{StaticResource AttachScrollBehaviorStyle}" />

    </ListBox.Resources>
</ListBox>
```
```csharp
private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
{
    // Tip: You can set the Magnitude property in the HorizontalScrollBehavior constructor.
    Interaction
        .GetBehaviors((ScrollViewer)sender)
        .Add(new HorizontalScrollBehavior(/* 0.5 | new Binding() | new MultiBinding() */));
}
```


## Events

| Event                                    | Handler Type             | EventArgs Type        |
|------------------------------------------|--------------------------|-----------------------|
| `HorizontalScroll.PreviewMouseWheelTilt` | `MouseWheelEventHandler` | `MouseWheelEventArgs` |
| `HorizontalScroll.MouseWheelTilt`        | `MouseWheelEventHandler` | `MouseWheelEventArgs` |

### Example

```xaml
<TextBox
    x:Name="MyTextBox"
    HorizontalScroll.PreviewMouseWheelTilt="MyTextBox_PreviewMouseWheelTilt"
    HorizontalScroll.MouseWheelTilt="MyTextBox_PreviewMouseWheelTilt" />
```

```csharp
private void TextBox_PreviewMouseWheelTilt(object sender, MouseWheelEventArgs e)
{
    // do something...
    // setting e.Handled to true here will prevent the MouseWheelTilt event from firing.
}
private void TextBox_MouseWheelTilt(object sender, MouseWheelEventArgs e)
{
    // do something...
}
```
