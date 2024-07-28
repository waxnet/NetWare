package console

// libraries
import (
	"fmt"

	"github.com/TwiN/go-color"
	"github.com/eiannone/keyboard"
)

// methods
func Out(text, selectedColor string) {
	fmt.Println(color.Colorize(colorList[selectedColor], text))
}

func Inp(text, selectedColor string) rune {
	fmt.Print(color.Colorize(colorList[selectedColor], text))

	character, _, status := keyboard.GetSingleKey()
	if status != nil {
		return rune(0)
	}

	return character
}

func Err(errorCode string) {
	fmt.Print(color.Colorize(colorList["red"], "Error Code : "+errorCode))
	fmt.Print(color.Colorize(colorList["gray"], "\n\nPress any key to exit . . ."))
	keyboard.GetSingleKey()
}
