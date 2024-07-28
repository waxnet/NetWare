@echo off

del NetWareSpoofer.exe > nul
garble -tiny -literals -seed=random build
rename spoofer.exe NetWareSpoofer.exe > nul
