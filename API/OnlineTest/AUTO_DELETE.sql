﻿--delete from RToken where Created_Date <= CURRENT_TIMESTAMP;
--select temp = Max(Id) from RToken;
--DBCC CHECKINDEX ( RToken,RESEED,temp);
--update RToken set Is_Stop = 1 where Created_Date <= CURRENT_TIMESTAMP;