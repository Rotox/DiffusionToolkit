# What's New in v1.10.5

**Search now also matches filenames**

Plain text searches in the search bar previously matched against prompt content only. Searches now also match against the image filename, allowing full or partial filename searches or seed fragment to return all matching results without requiring any special syntax.

**Right-click rename for image files**

Images can now be renamed directly from the thumbnail context menu. Right-click any image and choose Rename to edit the filename without the extension. The rename updates both the file on disk and the database record atomically. Invalid characters and filename conflicts are validated before the rename is applied.

**Modified date filter**

 A new Modified Date filter has been added with the same interface, allowing images to be filtered by their last modified timestamp. Selecting Between on either filter reveals a second date picker for range-based filtering. The Created Date filter has been updated with a new mode selector supporting Before, After, On, and Between.
 
 Both filters include full dark theme support across day numbers, day headers, navigation arrows, and the month/year display.

**Fixed model name display in image detail panel**

Now reads from PNG metadata stored in the database instead of looking up checkpoint files on disk; models no longer present on disk now display correctly
 
**Note:**  
After updating, run Tools → Rebuild Metadata to ensure model names are correctly synced in the database. This is required for model filtering to work correctly with the new metadata-based name resolution.

**Tag exclude filter**
A new exclude filter has been added to the sidebar allowing negative tag filtering. When a tag is selected, a Not option appears that excludes images matching that tag from search results.

**New Values Added to Metadata Panel**

- Refiner Model and Switch Point  
- LoRA Name and Weight

**Note:**  
New sections are hidden automatically when values are not present in the image metadata.