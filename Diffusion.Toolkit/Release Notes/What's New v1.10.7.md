# What's New in v1.10.7

**Multi-value Model Name filter**

The Model Name filter can now match multiple models at once. Click the chevron at the end of the Model Name row to expand it into a row editor — add as many conditions as you like, each with its own operator:

* or — match any of the listed models
* not — exclude models
* is — exact model name match
* contains — partial text match (e.g. "cyber" matches all Cyber-family checkpoints)

The collapsed single-value field works exactly as before. Clear resets back to it.

**LoRA filter**

Filter images by which LoRAs they use. A new LoRA row on the Metadata filter tab supports multiple conditions with and / or / not logic (an image can use several LoRAs, so "and" finds images using all of them). A No LoRA checkbox finds images that don't use any LoRA at all.

Your existing library is indexed automatically on first launch — LoRA names are read from image prompts, matching exactly what the metadata panel already displays. Newly scanned images are indexed as they're added.

## Fixes

* Fixed the contains operator matching exact names only instead of partial text
* Fixed the LoRA filter returning unfiltered results when used without expanding the row editor
* Fixed a stuck database migration (Scheduler column) that silently prevented any newer migrations from running
* Fixed a crash when refreshing an unavailable folder after a sibling folder was removed
