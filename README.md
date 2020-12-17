# HumanTimeParser

This lib is (kinda) capable of reading human time formats.

# Capabilities

This library is capable of ignoring english and extracting a correct time.

Parsing `30 minutes after 6:55am on 5/20/2021` will result in a DateTime object that represents 7:25am on 5/20/2021 (<- the lib can also parse that).

The lib cannot read `30 minutes before 6:55am on 5/20/2021`.  Parsing this string will result in the same DateTime as the previous example.

# Parsing

The lib can parse the following formats:

```
Seconds (double)
Minutes (double)
Hours (double)
Days (double)
Weeks (double)
Months (int)
Years (int)
```

The keywords can be found in [Constants](https://github.com/Zackattak01/HumanTimeParser/blob/main/HumanTimeParser/Constants.cs)
