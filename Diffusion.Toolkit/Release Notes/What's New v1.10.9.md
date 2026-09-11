# What's New in 1.10.9

## Diffusion Toolkit Enhanced

The application has been renamed to **Diffusion Toolkit Enhanced** to make it
clearer that this is a personal fork rather than the original. Nothing about
how it works has changed — same database, same folders, same settings.

## Two dates in the metadata panel

The metadata panel used to show a single field called "Date" without saying
which date it was. It now shows both:

- **Date Created** — when the image file first appeared on your drive
- **Date Modified** — the last time anything wrote to the file

Both are read directly from the file each time you select an image, so they
always reflect its current state on disk.

Worth knowing: if you run scripts that rewrite image metadata after
generation, the modified date will reflect when the script last ran, not when
the image was made. Date Created is the one that tracks generation time.

## Filter dropdowns stay current

The Model Name and LoRA filter dropdowns used to need an application restart
before newly scanned models and LoRAs would appear. They now refresh
automatically once a scan finishes, and when new images are picked up from a
watched folder.

## Update checks

Update checking is more reliable and no longer shows an error at startup when
it can't reach GitHub.
