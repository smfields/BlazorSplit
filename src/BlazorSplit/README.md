# BlazorSplit [![NuGet](https://img.shields.io/nuget/v/smfields.BlazorSplit)](https://www.nuget.org/packages/smfields.BlazorSplit/)
> Blazor wrapper around the [Split.js](https://github.com/nathancahill/split) library.

- **Zero Deps**
- **Fast:** No overhead or attached window event listeners, uses pure CSS for resizing.
- **Unopinionated:** Only compute view sizes. Everything else is up to you.

View the base library documentation [here](https://github.com/nathancahill/split/tree/master/packages/splitjs#readme)

## Table of Contents

- [Installation](#installation)
- [Documentation](#documentation)
- [Important Note](#important-note)
- [Options](#options)
- [Flexbox](#flex-layout)
- [CSS](#css)
- [Browser Support](#browser-support)
- [License](#license)
- [Base Library License](#splitjs-license)

## Installation

[![NuGet](https://img.shields.io/nuget/v/smfields.BlazorSplit)](https://www.nuget.org/packages/smfields.BlazorSplit/)
```bash
dotnet add package BlazorIntersectionObserver --version 3.1.0
```

In your `Program.cs` or `Startup.cs` use the following to register the required services:
```csharp
builder.Services.AddBlazorSplit();
```

## Documentation

### Basic Use
```razor
<Split>
    <SplitSection></Split>
    <SplitSection></Split>
    ...
</Split>
```
| Options        | Type            | Default        | Description                                              |
| -------------- | --------------- | -------------- | -------------------------------------------------------- |
| `Sizes`        | Number[]        |                | Initial sizes of each element in percents or CSS values. |
| `MinSize`      | NumberOrArray   | `100`          | Minimum size of each element.                            |
| `MaxSize`      | NumberOrArray   | `Infinity`     | Maximum size of each element.                            |
| `ExpandToMin`  | bool            | `false`        | Grow initial sizes to `minSize`                          |
| `GutterSize`   | Number          | `10`           | Gutter size in pixels.                                   |
| `GutterAlign`  | string          | `'Center'`     | Gutter alignment between elements.                       |
| `SnapOffset`   | Number          | `30`           | Snap to minimum size offset in pixels.                   |
| `DragInterval` | Number          | `1`            | Number of pixels to drag.                                |
| `Direction`    | string          | `'Horizontal'` | Direction to split: horizontal or vertical.              |
| `Cursor`       | string          | `'col-resize'` | Cursor to display while dragging.                        |
| `Class`        | string          |                | CSS class name for the container element.                |
| `Style`        | string          |                | CSS styles applied to the container element.             |

## Important Note
Split.js does not set CSS beyond the minimum needed to manage the width or height of the elements.
This is by design. It makes Split.js flexible and useful in many different situations.
If you create a horizontal split, you are responsible for (likely) floating the elements and the gutter,
and setting their heights. See the [CSS](#css) section below. If your gutters are not showing up, check the applied CSS styles.
**THIS IS THE #1 QUESTION ABOUT THE LIBRARY**.

## Options

#### Sizes
An array of initial sizes of the elements, specified as percentage values. Example: Setting the initial sizes to `25%` and `75%`.
```razor
<Split
    Sizes="new Number[]{ 25, 75 }"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### MinSize. Default: `100`
An array of minimum sizes of the elements, specified as pixel values. Example: Setting the minimum sizes to `100px` and `300px`, respectively.
```razor
<Split
    MinSize="new Number[]{ 100, 300 }"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### MaxSize. Default: `Infinity`
An array of maximum sizes of the elements, specified as pixel values. Example: Setting the maximum sizes of the first element to `500px`, and not setting a maximum size on the second element.
```razor
<Split
    MaxSize="new Number[]{ 500, Number.PositiveInfinity }"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

If a number is passed instead of an array, all elements are set to the same maximum size:
```razor
<Split
    MaxSize="500"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### ExpandToMin. Default: `false`
When the split is created, if `ExpandToMin` is `true`, the minSize for each element overrides the percentage value from the `Sizes` option.
Example: The first section is set to 25% width of the parent container. However, it's `MinSize` is `300px`. Using `ExpandToMin: true` means that the first element will always load at at least `300px`, even if `25%` were smaller.
```razor
<Split
    Sizes="new Number[]{ 25, 75 }"
    MinSize="new Number[]{ 300, 100 }"
    ExpandToMin="true"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### GutterSize. Default: `10`
Gutter size in pixels. Example: Setting the gutter size to `20px`.
```razor
<Split
    GutterSize="20"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### GutterAlign. Default: `'Center'`
Possible options are `'Start'`, `'End'` and `'Center'`. Determines how the gutter aligns between the two elements.
`'Start'` shrinks the first element to fit the gutter, `'End'` shrinks the second element to fit the gutter and `'Center'` shrinks both elements by the same amount so the gutter sits between.

Example: move gutter to the side of the second element:
```razor
<Split
    GutterAlign="GutterAlign.End"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### SnapOffset. Default: `30`
Snap to minimum size at this offset in pixels. Example: Set to `0` to disable to snap effect.
```razor
<Split
    SnapOffset="0"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### DragInterval. Default: `1`
Drag this number of pixels at a time. Defaults to `1` for smooth dragging, but can be set to a pixel value to
give more control over the resulting sizes. Works particularly well when the `GutterSize` is set to the same size.

Example: Drag 20px at a time:
```razor
<Split
    SnapOffset="20"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### Direction. Default: `'Horizontal'`
Direction to split in. Can be `'Vertical'` or `'Horizontal'`. Determines which CSS properties are applied (ie. width/height) to each element and gutter. Example: split vertically:
```razor
<Split
    Direction="Direction.Vertical"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### Cursor. Default: `'col-resize'`
Cursor to show on the gutter (also applied to the body on dragging to prevent flickering). Defaults to `'col-resize'`for `Direction: 'Horizontal'` and `'row-resize'` for `Direction: 'Vertical'`:
```razor
<Split
    Direction="Direction.Vertical"
    Cursor="row-resize"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

#### Class
Class name to apply to the container element
```razor
<Split
    Class="split-container"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

The example above would result in the following HTML
```html
<div class="split-container">
    <div class="split-section"></div>
    <div class="gutter gutter-horizontal">
    <div class="split-section"></div>
</div>
```

#### Style
Styles to apply to the container element
```razor
<Split
    Style="display: flex;"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```

The example above would result in the following HTML
```html
<div style="display: flex;">
    <div class="split-section"></div>
    <div class="gutter gutter-horizontal">
    <div class="split-section"></div>
</div>
```

## Flex Layout
Flex layout is supported easily by adding a `display: flex` to the parent element. The `width` or `height` CSS values
assigned by default by Split.js work well with flex.
```razor
<Split
    Class="split"
>
    <SplitSection></SplitSection>
    <SplitSection></SplitSection>
</Split>
```
And CSS style like this:
```css
.split {
    display: flex;
    flex-direction: row;
}
```

## CSS
In being non-opionionated, the only CSS Split.js sets is the widths or heights of the elements. Everything else is left up to you. You must set the elements and gutter heights when using horizontal mode. The gutters will not be visible if their height is 0px. Here's some basic CSS to style the gutters with, although it's not required. Both grip images are included in this repo:
```css
.gutter {
    background-color: #eee;
    background-repeat: no-repeat;
    background-position: 50%;
}
.gutter.gutter-horizontal {
    background-image: url('grips/vertical.png');
    cursor: col-resize;
}
.gutter.gutter-vertical {
    background-image: url('grips/horizontal.png');
    cursor: row-resize;
}
```
The grip images are small files and can be included with base64 instead:
```css
.gutter.gutter-vertical {
    background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAFAQMAAABo7865AAAABlBMVEVHcEzMzMzyAv2sAAAAAXRSTlMAQObYZgAAABBJREFUeF5jOAMEEAIEEFwAn3kMwcB6I2AAAAAASUVORK5CYII=');
}
.gutter.gutter-horizontal {
    background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAeCAYAAADkftS9AAAAIklEQVQoU2M4c+bMfxAGAgYYmwGrIIiDjrELjpo5aiZeMwF+yNnOs5KSvgAAAABJRU5ErkJggg==');
}
```
Split.js also works best when the elements are sized using `border-box`. The `split` class would have to be added manually to apply these styles:
```css
.split {
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;
}
```
And for horizontal splits, make sure the layout allows elements (including gutters) to be displayed side-by-side. Floating the elements is one option:
```css
.split,
.gutter.gutter-horizontal {
    float: left;
}
```
If you use floats, set the height of the elements including the gutters. The gutters will not be visible otherwise if the height is set to 0px.
```css
.split,
.gutter.gutter-horizontal {
    height: 300px;
}
```
Overflow can be handled as well, to get scrolling within the elements:
```css
.split {
    overflow-y: auto;
    overflow-x: hidden;
}
```

## Browser Support
This library uses [CSS calc()](https://developer.mozilla.org/en-US/docs/Web/CSS/calc#AutoCompatibilityTable), [CSS box-sizing](https://developer.mozilla.org/en-US/docs/Web/CSS/box-sizing#AutoCompatibilityTable) and [JS getBoundingClientRect()](https://developer.mozilla.org/en-US/docs/Web/API/Element/getBoundingClientRect#AutoCompatibilityTable). These features are supported in the following browsers:
| <img src="http://i.imgur.com/dJC1GUv.png" width="48px" height="48px" alt="Chrome logo"> | <img src="http://i.imgur.com/o1m5RcQ.png" width="48px" height="48px" alt="Firefox logo"> | <img src="http://i.imgur.com/8h3iz5H.png" width="48px" height="48px" alt="Internet Explorer logo"> | <img src="http://i.imgur.com/iQV4nmJ.png" width="48px" height="48px" alt="Opera logo"> | <img src="http://i.imgur.com/j3tgNKJ.png" width="48px" height="48px" alt="Safari logo"> | [<img src="https://i.imgur.com/29eVTCg.png" height="28px" alt="Sauce Labs">](https://saucelabs.com) |
| :-------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------: | :-------------------------------------------------------------------------------------: | :-------------------------------------------------------------------------------------------------- |
|                                          22+ ✔                                          |                                           6+ ✔                                           |                                                9+ ✔                                                |                                         15+ ✔                                          |                                         6.2+ ✔                                          | Sponsored ✔                                                                                         |
Cross-browser Testing Platform and Open Source <3 Provided by [Sauce Labs](https://saucelabs.com).

## License
Copyright (c) 2022 Sam Fields
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

## SplitJS License
Copyright (c) 2019 Nathan Cahill
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
