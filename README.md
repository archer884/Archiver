Archiver
========

So basically a pal of mine at an old job was complaining that he couldn't figure out a good way to do this. I wrote two ways to do it: one in Powershell and one in C#.

My evil plan at this point is basically to convert this to a DLL that you can call from Powershell that does the job correctly. Right now, the Powershell variant is ... Less than reliable, shall we say, because the IDE you use for that kind of thing--and the language itself, really--doesn't lend itself to catching certain categories of errors ahead of time, so to speak. 

This version works pretty darned perfectly, but the configuration is all done in XML, which isn't necessarily the most entertaining thing to work with. Actually, there is only really one thing missing that I might go ahead and change before I head out... 

## 2013/12/23
Added: Configuration option for or "filter" in config file.

## Future plans:
- Make this available as a service?
