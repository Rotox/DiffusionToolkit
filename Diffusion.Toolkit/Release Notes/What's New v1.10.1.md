# What's New in v1.10.1

## Fixed model name display
Model names are now read directly from PNG metadata stored in the database instead of looking up checkpoint files on disk. Models no longer present on disk now display correctly.

## Tag exclude (Not) filter
Added a Not checkbox to sidebar tag rows for negative tag filtering in search. Allows filtering for images tagged with one tag but not another (e.g. show `reviewed` but not `published`).
