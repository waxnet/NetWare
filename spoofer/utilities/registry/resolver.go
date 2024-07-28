package registry

// libraries
import (
	"strings"

	"golang.org/x/sys/windows/registry"
)

// methods
func FindRefreshTokenKey() string {
	// open 1v1.lol registry key
	registry, registryStatus := registry.OpenKey(registry.CURRENT_USER, "SOFTWARE\\JustPlay.LOL\\1v1.LOL", registry.READ)
	if registryStatus != nil {
		return ""
	}
	defer registry.Close()

	// get registry key values
	registryValues, registryValuesStatus := registry.ReadValueNames(-1)
	if registryValuesStatus != nil {
		return ""
	}

	// find value
	for _, registryValue := range registryValues {
		if strings.Contains(registryValue, "firebase_refresh_token") {
			return registryValue
		}
	}

	// return default if not found
	return "firebase_refresh_token_h1193372590"
}

func FindSignInPlatformKey() string {
	// open 1v1.lol registry key
	registry, registryStatus := registry.OpenKey(registry.CURRENT_USER, "SOFTWARE\\JustPlay.LOL\\1v1.LOL", registry.READ)
	if registryStatus != nil {
		return ""
	}
	defer registry.Close()

	// get registry key values
	registryValues, registryValuesStatus := registry.ReadValueNames(-1)
	if registryValuesStatus != nil {
		return ""
	}

	// find value
	for _, registryValue := range registryValues {
		if strings.Contains(registryValue, "FirebaseSignInPlatform") {
			return registryValue
		}
	}

	// return default if not found
	return "FirebaseSignInPlatform_h2279995555"
}
