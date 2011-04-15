# ItsBeen #

It's a small app to track the last time you did something. This could be
anything of your choosing. Some examples include the last time you took a
vitamin or updated your resume.

## Background ##

I originally started this as a learning exercise using Prism. I figured it
would be a more interesting lesson if I implemented something half-way useful.

The inspiration for the app came from the ItzBeen baby reminder, which you can
[Google yourself][1]. I don't have kids, but even so I thought the device would
be immensely useful, particularly to forgetful people like myself.

Since starting it, I've worked on creating a Windows Phone 7 and WPF (sans
Prism) version. These are still a learning exercise in MVVM and modularity.
I've purposely tried to divide the parts to be re-usable across both platforms.

## Code ##

Since this is supposed to aid in my own learning, I thought I would make it
public for others to see. I don't claim to be an expert or anything, but this
*does* work and it's a good example of how to share code across very different
platforms.

As you may already know, while WP7 utilizes .NET it is a specific flavor, and
so there are significant differences in the UI and implementation that cannot
be shared. But a surprising amount of logic can be shared, and the beauty of
the MVVM pattern is that it easily allows this sharing of code.

## Details ##

- Project created in Visual Studio 2010
- .NET 4.0 Client Profile / .NET for Windows Phone 7
- Uses the [MVVM Light][2] toolkit by Laurent Bugnion
- WPF client uses my [Cosmopolitan for WPF][3] theme and [Task Dialog][4]
- Licensed under [Creative Commons BY-NC-SA 3.0][5]
    - Must attribute
	- Non-commercial
	- Share-alike

[1]: http://www.google.com/#hl=en&q=itzbeen
[2]: http://mvvmlight.codeplex.com/
[3]: http://github.com/yadyn/Cosmopolitan-Theme-for-WPF
[4]: http://www.codeproject.com/KB/WPF/WPFTaskDialogEmulator.aspx
[5]: http://creativecommons.org/licenses/by-nc-sa/3.0/