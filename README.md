![horizontalscroll-banner-color-outline](https://github.com/radj307/HorizontalScroll/assets/1927798/d2560e6b-159c-4663-a466-c3701c4154a1)

NuGet package for **.NET 6+** that adds attached events for handling inputs from tiltable scroll wheels and provides an easy way to improve scrolling behavior in your application.
It's lightweight, easy to use, and provides documentation through intellisense.

*Requires the [**Microsoft.Xaml.Behaviors.Wpf**](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) nuget package.*

| NuGet                | Version                                                                                                                                                         |
|----------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **HorizontalScroll** | [![NuGet Status](http://img.shields.io/nuget/v/HorizontalScroll.svg?style=flat-square&logo=nuget&label=NuGet)](https://www.nuget.org/packages/HorizontalScroll) |

# Features

- New events for handling tilt wheel inputs:
  - `HorizontalScroll.PreviewMouseWheelTilt`
  - `HorizontalScroll.MouseWheelTilt`
- New behavior for `ScrollViewer` controls:
  - Scrolls horizontally when tilting the mouse wheel
  - `Shift+ScrollWheel` scrolls horizontally
  - Scrolling sensitivity can be set or data-bound

# Usage

> 1. [`HorizontalScrollBehavior`](#behaviors)
>    1. [Enabling Application-Wide](#enabling-application-wide-support)
> 2. [Events](#events)

***Note:** No `xmlns` declarations are required to use **HorizontalScroll** in XAML.*

## Behaviors

`HorizontalScrollBehavior` can be attached to any `ScrollViewer` control, and adds support for scrolling horizontally by tilting the mouse wheel or by holding the `Shift` key while scrolling up or down.  

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

    <!-- ... -->
</ScrollViewer>
```
Scrollable WPF controls like `ListBox`, `ListView`, `TreeView`, `DataGrid`, etc. are implemented using a `ScrollViewer`, so you can use `HorizontalScrollBehavior` to add support for them as well. 
While you *could* use a `ControlTemplate` to accomplish that, a better way is to use an `EventSetter` to attach it in the `Loaded` event:  
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

**HorizontalScroll** adds two attached events for all subclasses of `UIElement`:

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
