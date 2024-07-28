package network

// libraries
import (
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"strings"
)

// values
type responseStruct struct {
	Kind         string `json:"kind"`
	IDToken      string `json:"idToken"`
	RefreshToken string `json:"refreshToken"`
	ExpiresIn    string `json:"expiresIn"`
	LocalID      string `json:"localId"`
}

// methods
func GenerateAccount() (string, bool) {
	// make account generation request
	generationRequest, status := http.Post(
		"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=AIzaSyBPrAfspM9RFxuNuDtSyaOZ5YRjDBNiq5I&returnSecureToken=true",
		"application/json",
		strings.NewReader(`{"key": "value"}`),
	)
	if status != nil {
		return "", false
	}
	defer generationRequest.Body.Close()

	// read request content
	content, status := io.ReadAll(generationRequest.Body)
	if status != nil {
		return "", false
	}

	// parse json
	var jsonResponse responseStruct
	if status := json.Unmarshal([]byte(content), &jsonResponse); status != nil {
		return "", false
	}

	// remove age gate
	ageRequest, status := http.Post(
		fmt.Sprintf("https://us-central1-justbuild-cdb86.cloudfunctions.net/v460_ageGate/setAge?age=20&Auth-Token=%s", jsonResponse.IDToken),
		"application/json",
		strings.NewReader(`{"key": "value"}`),
	)
	if status != nil {
		return "", false
	}
	defer ageRequest.Body.Close()

	// return data
	return jsonResponse.RefreshToken, true
}
