package console

// libraries
import (
	"os"
	"os/exec"
	"strconv"
	"syscall"
	"unsafe"
)

// values
var Width = 0
var Height = 0

// methods
func SetSize(cols, lines int) {
	Width = cols
	Height = lines

	exec.Command("cmd", "/c", "mode", "con:", "cols="+strconv.Itoa(cols), "lines="+strconv.Itoa(lines)).Run()
}

func Clear() {
	command := exec.Command("cmd", "/c", "cls")
	command.Stdout = os.Stdout
	command.Run()
}

func SetTitle(title string) {
	kernel32 := syscall.NewLazyDLL("kernel32.dll")
	procSetConsoleTitle := kernel32.NewProc("SetConsoleTitleW")

	titlePointer, _ := syscall.UTF16PtrFromString(title)

	procSetConsoleTitle.Call(uintptr(unsafe.Pointer(titlePointer)))
}
