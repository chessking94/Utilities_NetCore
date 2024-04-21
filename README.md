# PreparationDownload_VB

This project downloads all games of selected users in the Chess.com and Lichess
online archives, merge the files, sort the games by date, and clean for common
issues.

I utilize the free software pgn-extract in order to clean game files. This software
and its documentation can be found at https://www.cs.kent.ac.uk/people/staff/djb/pgn-extract/.
I take no credit for its use.

Originally I had written this in Python and to be used via the command line/with
a config file, but after a while and learning new languages I felt it was time
to rewrite it in such a way that it is a true application.
