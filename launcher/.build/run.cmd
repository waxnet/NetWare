@echo off

nuitka --standalone --windows-uac-admin --onefile --lto=yes --remove-output --enable-plugin=tk-inter --windows-console-mode=disable --windows-icon-from-ico=icon.ico ../main.py
