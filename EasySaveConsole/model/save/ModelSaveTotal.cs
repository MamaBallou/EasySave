using EasySaveConsole.model.enums;
using EasySaveConsole.model.log;

namespace EasySaveConsole.model.save
{
    /// <summary>
    /// Total save class, extends from ModelSave.
    /// </summary>
    public class ModelSaveTotal : ModelSave
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Save name.</param>
        /// <param name="sourceFile">Source path.</param>
        /// <param name="targetFile">Target path.</param>
        public ModelSaveTotal(string name, string sourceFile, string targetFile)
            : base(name, sourceFile, targetFile) { }

        /// <summary>
        /// Check if file is to be saved.
        /// </summary>
        /// <param name="sourceFile">Source file path.</param>
        /// <param name="targetPath">Target directory path</param>
        /// <returns>Allways true for Total save.</returns>
        protected override bool checkIfToSave(string sourceFile,
            string targetPath)
        {
            return true;
        }

        public override ModelState toModelState()
        {
            return new ModelState(this.name, this.sourcePath, this.targetPath, EnumSaveTypes.Total);
        }
    }
}
