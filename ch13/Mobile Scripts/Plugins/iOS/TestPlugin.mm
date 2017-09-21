#import "TestPlugin.h"

@implementation TestObject
@end

// Converts C style string to NSString
// http://answers.unity3d.com/questions/387371/passing-a-value-from-unity-to-objective-c.html
NSString* CreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
// http://forum.unity3d.com/threads/66435-Plugins-Sending-String-Data
char* MakeStringCopy (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
    const char* _TestString(const char* string) {
        NSString* oldString = CreateNSString(string);
        NSString* newString = [oldString uppercaseString];
        return MakeStringCopy([newString UTF8String]);
    }
    
    float _TestNumber() {
        return (arc4random() % 100)/100.0f;
    }
}