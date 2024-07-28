package registry

// libraries
import (
	"golang.org/x/sys/windows/registry"
)

// methods
func SetRefreshToken(valueName, value string) bool {
	// open 1v1.lol registry key
	registry, status := registry.OpenKey(registry.CURRENT_USER, "SOFTWARE\\JustPlay.LOL\\1v1.LOL", registry.ALL_ACCESS)
	if status != nil {
		return false
	}
	defer registry.Close()

	// set value
	if status := registry.SetBinaryValue(valueName, []byte(value)); status != nil {
		return false
	}
	return true
}

func SetSignInPlatform(valueName string) bool {
	// open 1v1.lol registry key
	registry, status := registry.OpenKey(registry.CURRENT_USER, "SOFTWARE\\JustPlay.LOL\\1v1.LOL", registry.ALL_ACCESS)
	if status != nil {
		return false
	}
	defer registry.Close()

	// set value
	if status := registry.SetBinaryValue(valueName, []byte{0x47, 0x6F, 0x6F, 0x67, 0x6C, 0x65, 0x00}); status != nil {
		return false
	}
	return true
}
