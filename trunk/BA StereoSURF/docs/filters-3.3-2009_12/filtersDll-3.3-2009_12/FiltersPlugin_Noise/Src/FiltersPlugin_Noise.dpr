library FiltersPlugin_Noise;

uses
  SysUtils,
  Classes,
  main in 'main.pas',
  dllInterface in 'dllInterface.pas',
  image in 'image.pas';

{$R *.res}
exports
  getVersion,
  createFilter, deleteFilter,
  run, runCommand,
  getParametersCount, getParameterName, getParameterHelp,
  setParameterInteger, setParameterFloat,
  setParameterBoolean, setParameterString,
  setParameterImage, setParameterPointer,
  setParameterImagesCount, setParameterImagesImageAtIndex,
  getOutputsCount, getOutputName,
  getOutputImage, getOutputImagesCount, getOutputImagesImageAtIndex,
  getOutputInteger, getOutputFloat,
  getOutputArrayPointersCount, getOutputArrayPointersPointerAtIndex,
  setRegionOfInterest, unsetRegionOfInterest;

begin
end.
