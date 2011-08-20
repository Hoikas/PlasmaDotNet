using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    /// <summary>
    /// Delegate used by operation progresses (ie progress bars) to increment the status
    /// </summary>
    /// <param name="total">The total number of tasks in this operation</param>
    /// <param name="curr">The number of tasks completed in this operation</param>
    public delegate void plOperationProgress(int total, int curr);

    /// <summary>
    /// Delegate used by operation progresses to inform the callee about the operation
    /// </summary>
    /// <param name="type">What kind of operation we are performing</param>
    /// <param name="tag">Any generic data associated with the operation</param>
    public delegate void plOperationDescriptor(plOperationType type, object tag);

    public enum plOperationType {
        ReadingPageObjects,
        WritingPageObjects,
        ReadingPages,
    }
}
