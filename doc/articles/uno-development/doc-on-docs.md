# Contributing docs to Uno Platform

Good documentation is essential, and writing docs is a valued contribution that makes Uno more useful for everybody. This article covers *when* to write docs and *what* you should write, with a focus on two of the most common forms of documentation: step-by-step guides (ie howtos or tutorials), and feature documentation.

We have a few different fill-in-the-blanks style templates, linked below.

For the DocFX tool used to build the docs website, see [more info here](docfx.md).

## Step-by-step guides

Step-by-step guides that address a particular problem or use case that multiple developers are likely to encounter. 

 * For longer tutorials, it's fine to split the content over multiple pages.
 * Guides should always be accompanied by working code. Standalone applications should be added to the [Uno.Samples repository](https://github.com/unoplatform/Uno.Samples) and linked to from the associated tutorial.
 * Use the [howto template](https://github.com/unoplatform/uno/blob/master/doc/.howto-template.md) as a starting point.

## Documenting features

Let's say you're implementing a new Uno Platform feature. Do you need to add documentation for it?

It depends.

There's two different cases:

### I'm implementing a feature from the WinUI/UWP contract

Uno's API matches WinUI's API, and most of the time, a new Uno feature will map to an existing WinUI feature.

In this case, to the extent that the behavior you're adding is the same as the WinUI behavior, you **don't** need to add documentation. The existing WinUI documentation is fine. It's already linked to from [Uno's reference documentation](../implemented-views.md).

What if the Uno behavior deviates from WinUI behavior?

If it's just a case of part of the functionality not being implemented yet, **and** the developer using the corresponding type/method/event would discover that it's marked with the `[NotImplemented]` attribute, then that's fine - there's no need to document it further.

But sometimes the functionality **can't** be supported, due to intrinsic limitations of the target platform. (This is more likely to be the case with non-UI features.) And/or part of the functionality may implicitly fail to work, even though none of the entry points are marked `NotImplemented`. 

In those cases, it's important to add documentation. Copy the [Uno-only feature template here](https://github.com/unoplatform/uno/blob/master/doc/.feature-template-uno-only.md) to the [features directory](https://github.com/unoplatform/uno/tree/master/doc/articles/features) (or [controls directory](https://github.com/unoplatform/uno/tree/master/doc/articles/controls) for controls inheriting from `FrameworkElement`) and fill in the appropriate sections. Make sure to fill in the matrix describing which features are supported on which platforms.

### I'm implementing a feature that's not part of WinUI

Rarely, features are added to Uno Platform that aren't part of WinUI (`VisibleBoundsPadding` and `ElevatedView` are two examples). Somewhat more commonly, platform-specific functionality or options are added to an existing feature.

It's important to document these novel features when you add them, since they aren't covered anywhere else. Again, copy the [WinUI feature template](https://github.com/unoplatform/uno/blob/master/doc/.feature-template.md) to the [features directory](https://github.com/unoplatform/uno/tree/master/doc/articles/features) and fill in the sections.