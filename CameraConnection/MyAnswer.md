** As a software engineer who has more experience, you are tasked to review the intern’s code **

1. What would you say about the above code?
2. What sorts of problems does this code have?
3. How can this code be improved?
4. If you were to be the one originally assigned to this task, or you are given the opportunity to
   reimplement from scratch, how would you do it? Provide a sample code.

** Answer **

1. My List of answer:

- I think this code is good. For class creation, the interns use interfaces make it possible to define multiple class implementation
- This makes the code scalable in the future if somehow a developer needs to create new implementation of a class, e.g perhaps creating new implementation for IValueReporter or IFrameCallback.
- It is also good practice for using event handler for passing data between public class FrameGrabber : IFrameCallback and public class FrameCalculateAndStream.
- The interns implement decoupling to make these two classes less dependable, easier to debug and open more for scalability. Perhaps in the future, a developer wants to create a new class that "subscribe" to public class FrameGrabber: IFrameCallback, the developer can just focus on the new class
- For new Frame object creation, since it has disposable property, it should extend IDisposable so the Frame object can be disposed. In this case, the intern implemented "Calling Dispose from a finalizer" as it mentioned in books "C# in a nutshell, the definitive reference" by Joseph Albahari in chapter 12

2. My list of answer:

- Each interface isn’t created in different file
- "class FrameGrabber" and "class FrameCalculateAndStream" aren't in different file
- "public class Frame" isn't created in different file
- "public void StartStreaming()" isn't called anywhere
- There isn't any implementation for "public interface IValueReporter"
- After each new Frame and Time Elapsed handler subscription, it should do unsubscribe for that event

3. Implement my answer from number two

4. This class library is my answer for this question. I implemented what are the problems from my answer in number two
