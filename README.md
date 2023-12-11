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
> 2. [Events](#events)

***Note:** No `xmlns` declarations are required to use **HorizontalScroll** in XAML.*

## Behaviors

### HorizontalScrollBehavior

`HorizontalScrollBehavior` is a `ScrollViewer` behavior that adds support for scrolling horizontally by tilting the mouse wheel or by holding the `Shift` key while scrolling up or down.  
It can be attached to a specific ScrollViewer in the usual way through XAML, or to all ScrollViewers in a scope *(including the ones inside of other controls, like a `ListBox` or `DataGrid`)* with a target-typed style and an `EventSetter`.

Attaching to a specific `ScrollViewer`:  
```xaml
<ScrollViewer xmlns:i="http://schemas.microsoft.com/xaml/behaviors">
    <i:Interaction.Behaviors>
        <HorizontalScrollBehavior />
    </i:Interaction.Behaviors>
</ScrollViewer>
```

To attach it to all of the `ScrollViewer` controls in a scope, create a target-typed style resource and an `EventSetter` for the `Loaded` event:  
```xaml
<Application.Resources>
    <Style x:Key="EnableHorizontalScrollStyle" TargetType="{x:Type ScrollViewer}">
        <EventSetter Event="Loaded" Handler="ScrollViewer_Loaded" />
    </Style>
</Application.Resources>
```
And attach the behavior in the code-behind:  
```csharp
private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
{
    // Tip: You can set the Magnitude property (to a value or binding) in the HorizontalScrollBehavior constructor.
    Interaction
        .GetBehaviors((ScrollViewer)sender)
        .Add(new HScroll.HorizontalScrollBehavior( /* magnitude: 0.5 | magnitudeBinding: new Binding() */ ));
}
```

You can change the scrolling sensitivity with the **Magnitude** DependencyProperty, and/or disable the Shift+ScrollWheel input binding with the **EnableShiftScroll** DependencyProperty.


### AttachHorizontalScrollBehavior

In situations where you want to apply a `HorizontalScrollBehavior` to a specific control's internal `ScrollViewer` without affecting list items, 
or in cases where the `ScrollViewer` isn't available when the behavior would normally be attached *(such as when setting `ListView.View`)*,
you can use `AttachHorizontalScrollBehavior` to attach it on the attached control's `Loaded` event.
It works by searching the descendants and/or ancestors of the control for a `ScrollViewer`, and attaches a `HorizontalScrollBehavior` to it.

```xaml
<ListView xmlns:i="http://schemas.microsoft.com/xaml/behaviors">
    <i:Interaction.Behaviors>
        <AttachHorizontalScrollBehavior />
    </i:Interaction.Behaviors>
    <ListView.View>
        <GridView>
            <GridViewColumn />
        </GridView>
    </ListView.View>
</ListView>
```

You can fine-tune the search behavior for finding the `ScrollViewer` with the `SearchMode` & `SearchDepth` properties.  
The search mode determines whether ancestors and/or descendants are searched, and in what order.  
The search depth determines how many layers of ancestor/descendant controls can be searched.


## Events

**HorizontalScroll** adds two attached events for all subclasses of `UIElement`:  

| Event                                    | Handler Type             | EventArgs Type        |
|------------------------------------------|--------------------------|-----------------------|
| `HorizontalScroll.PreviewMouseWheelTilt` | `MouseWheelEventHandler` | `MouseWheelEventArgs` |
| `HorizontalScroll.MouseWheelTilt`        | `MouseWheelEventHandler` | `MouseWheelEventArgs` |

You can get the horizontal delta value from `MouseWheelEventArgs.Delta`.  
A positive delta means the wheel was tilted right, negative values mean it was tilted left.  

Usually the delta value is `-120` *(left)* or `120` *(right)*, but this should not be relied upon as MSDN says other values are allowed in order to perform smaller scrolling increments.

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
