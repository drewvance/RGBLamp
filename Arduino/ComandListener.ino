  int blue = 9;
  int red = 10;
  int green =11;
  
  int current_led;
  char incomingByte;              
  char cmdMsg[100];  
  
  
  // the setup routine runs once when you press reset:
  void setup() {                 
    analogWrite(red, 255);
    analogWrite(blue, 255);    
    analogWrite(green, 255);
  
    Serial.begin(9600);   
  }
  
  byte nextByte() {
    while(1) {
      if(Serial.available() > 0) {
        byte b =  Serial.read();
        return b;
      }
    }
  }
  
  // the loop routine runs over and over again forever:
  void loop() {  
    char val[50];

    int cmd = nextByte();
    if(cmd == 126) {
      boolean startVal = false;
      int ival = 0;
        
      char charIn = 0;
      byte i = 0;
      while (charIn != 126) {  // wait for header byte again
        charIn = nextByte();
        if (charIn == 59) {
          startVal = true;
        }
        else if (startVal) {
          val[ival] = charIn;
          ival += 1;
        }
        
        if(charIn != 126) {
          cmdMsg[i] = charIn;
          i += 1;
        }          
      }
        Serial.println (cmdMsg); //Serial.println is important for C# (took me a while to figure that out)
          //that will shoot back whatever you sent in without the '~'s
    }
  
    if (cmdMsg[0] == 114) {
      current_led = red;
    }
    else if (cmdMsg[0] == 103) {
      current_led = green;
    }
    else if (cmdMsg[0] == 98) {
      current_led = blue;
    }
  
  
    if (current_led != -1) {
        analogWrite(current_led,  atoi(val));
        current_led = -1;        
    }
 }   
    
  

