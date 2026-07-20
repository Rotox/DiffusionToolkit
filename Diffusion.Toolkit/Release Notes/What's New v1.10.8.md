What's New in v1.10.8

Improvements

* Database updates are now applied one at a time with proper cleanup. If an update fails, it no longer silently blocks all future updates — the failure is rolled back, logged, and reported in a message at launch, while the app continues to run normally.


* The LoRA filter dropdown now only lists LoRAs that are still used by at least one image in your library. Entries left behind after deleting images no longer clutter the list.
