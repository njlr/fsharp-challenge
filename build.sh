#!/bin/bash
fsharpc CellularAutomata.fs CLI.fs Program.fs --out:Program.exe && chmod +x Program.exe
