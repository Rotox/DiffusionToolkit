# What's New in v1.10.6

**Selected file size in status bar**

When one or more images are selected, the status bar at the bottom of the screen now shows the file size of the selection alongside the existing result count. A single selected image shows its own size, and multiple selected images show the combined total. 

**Remove unavailable folders**

Folders deleted outside of Diffusion Toolkit can now be cleaned up directly from within the app. Go to Tools → Folders → Remove Unavailable Folders, select which root folders to scan, and any tracked subfolders that no longer exist on disk will be removed from the folder tree immediately. The existing Clean Removed Folders option has been renamed to Clean Orphaned Images to better reflect what it does.

**Scheduler now shown alongside sampler**

The metadata panel now displays the scheduler (e.g. Karras) next to the sampler name under a combined SCHEDULER header. Images without scheduler metadata are unaffected and continue to show the sampler name only.

## Fixes

Fixed null reference crashes when rating non-standard PNG files