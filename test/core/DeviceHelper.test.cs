
using static SimpleGameEngine.TestRunner;

namespace SimpleGameEngine;

[TestClass]
public class DeviceHelperTest {

    public void GivenDeviceHelper_whenFramesPerSecondIsSetToNegative_thenValueNotChange(){

    }

    public void GivenDeviceHelper_whenFramesPerSecondIsSetToZero_thenValueNotChange(){
        
    }

    public void GivenDeviceHelper_whenFramesPerSecondIsSetTo64_then64IsReturned(){
        
    }

    public void GivenDeviceHelperWithDimension800x600_whenGetDimensionWidthIsChanged_thenDeviceDimensionWidthIsNotChanged (){

    }

    public void GivenDeviceHelperWithDimension800x600_whenGetDimensionHeightIsChanged_thenDeviceDimensionHeightIsNotChanged (){

    }

    // the frames per second must be positive

    // the gets must not modify the instance
    
    // The loadr must not be called tow times to the same resource

    // the registers must capture only the selected element
    
    // changing game must reset the registers
}
